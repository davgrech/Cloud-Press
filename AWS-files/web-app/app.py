from flask import Flask, request, render_template, redirect, url_for, send_file
import boto3
import hashlib
import json
import base64
import os
app = Flask(__name__)

def start_boto3_session():
    session = boto3.Session(
    aws_access_key_id='XXXXXXXXXXXXX',
    aws_secret_access_key='XXXXXXXXXXXXXXXXXXXXXXXXXXXX',
    region_name='xx-xxxxx-x'
    )
    lambda_client = session.client('lambda')
    return lambda_client

def send_login(username, password, lambda_client):

    hashed_password = hashlib.md5(password.encode()).hexdigest()
    
    response = lambda_client.invoke(
    FunctionName='GetUserPy',
    Payload=json.dumps({'username': username, 'password': hashed_password})
    )
    return json.loads(response['Payload'].read().decode())

def get_filenames(lambda_client, username):
    filenames = []
    response = lambda_client.invoke(
    FunctionName='LambdaListAllFiles',
    Payload=json.dumps({'username': username})
    ) 
    json_str = response['Payload'].read().decode()    
    if "body" in json_str:
        filenames = (json.loads(json_str))['body']
    return filenames

def get_file_data(username, files, lambda_client):
    file_data = []
    response = lambda_client.invoke(
    FunctionName='LmbdaFetchS3',
    Payload=json.dumps({'username': username,
                        'files': files})
    ) 
    json_str = response['Payload'].read().decode()   
    file_data = (json.loads(json_str))['body']
    return file_data


def show_file_menu(filenames, username):
    html_list = "<ul>"
    for filename in filenames:
        html_list += f"<li><a href='/download/{username}/{filename}'>{filename}</a></li>"
    html_list += f"<button><a href='/upload/{username}/'>Upload Files</a></button></ul>"
    return html_list

def upload_file(username, file):
    lambda_client = start_boto3_session()
    #file.save(os.path.join('uploads', file.filename))
    filename = file.filename
    file_bytes = file.read()
    encoded_bytes = base64.b64encode(file_bytes)
    encoded_string = encoded_bytes.decode('utf-8')

    response = lambda_client.invoke(
    FunctionName='LambdaAddS3',
    Payload=json.dumps({'username': username,
                        'files': filename,
                        "file_data": encoded_string})
    ) 
    json_str = response['Payload'].read().decode()   
    upload_response = (json.loads(json_str))['body']
    return f"<b>{upload_response}</b"


def lambda_register(username, password):
    lambda_client = start_boto3_session()
    hashed_password = hashlib.md5(password.encode()).hexdigest()
    response = lambda_client.invoke(
    FunctionName='WriteUserPy',
    Payload=json.dumps({'username': username,
                        'password': hashed_password})
    ) 
    json_str = response['Payload'].read().decode()   
    reg_respone = (json.loads(json_str))['body']
    return f"<b>{reg_respone}</b>"


@app.route('/', methods=["POST", "GET"])
def index():
    if request.method == "POST":
        username = request.form['username']
        password = request.form['password']
        if not username or not password:
            return render_template("index.html")
        
        lambda_client = start_boto3_session()
        res = send_login(username, password, lambda_client)
        if res:
            filenames = get_filenames(lambda_client, username)
            html_list = show_file_menu(filenames, username)
            return html_list
        else:
            return render_template("index.html")
    else:
        return render_template("index.html")


@app.route('/download/<username>/<filename>', methods=["GET"])
def download_file(filename, username):
    lambda_client = start_boto3_session()
    # Fetch file data from Lambda function
    file_data = get_file_data(username, filename, lambda_client)[0]
    decoded_data = base64.b64decode(file_data)

    # Create a temporary file to store the data
    #filepath = os.path.join('tmp', filename)
    with open(filename, 'wb') as f:
        f.write(decoded_data)
        f.close()
    return send_file(f'{filename}', as_attachment=True)


@app.route('/upload/<username>/', methods=['GET', 'POST'])
def upload_file_main(username):
    if request.method == 'POST':
        file = request.files['file']
        upload_file(username, file)
        return 'File uploaded successfully!'
    return render_template('upload.html')

@app.route('/register/', methods=['GET', 'POST'])
def register():
    if request.method == 'POST':
        username = request.form['username']
        password = request.form['password']
        return lambda_register(username, password)
    return render_template('register.html')
    
if __name__ == '__main__':
    app.run(debug=True)