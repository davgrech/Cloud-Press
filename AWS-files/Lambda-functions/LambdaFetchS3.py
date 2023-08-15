import boto3
import base64

def parse_request(event):
    paths = []
    folder = event['username']
    substring = ""
    files = event['files']
    files = files.split(", ")
    for file_names in files:
        paths.append(folder + "/" + file_names)
    return paths
    
    
def get_files(paths):
    returned_files = []
    s3 = boto3.client('s3')
    for file_path in paths:
        try:
            file_obj = s3.get_object(Bucket='cpfileuploads', Key=file_path)
        except:
            break
        if file_obj:
            file_content = file_obj['Body'].read()
            returned_files.append(base64.b64encode(file_content).decode('utf-8'))
    print(len(returned_files))
    return {
        'statusCode': 200,
        'body': returned_files
    }
    

def lambda_handler(event, context):
    paths = parse_request(event)
    chosen_files = get_files(paths)
    
    return chosen_files
    
    
    
    
    
    

    

