import boto3
import base64

s3 = boto3.client('s3')
bucket_name = 'cpfileuploads' 
folder_name = 'david'

 # If filenames and data arent the same
def check_compatibility(file_data, paths):
    if (file_data) != (paths):
        return False
    return True

# check if a directory in s3 bucket exists for user
# if it doesnt - it creates a new one
def check_for_dir(username):
    try:
        s3.head_object(Bucket=bucket_name, Key=f'{username}/')
        return {'statusCode': 200, 'body': f'Folder {username} exists.'}
    except:
        s3.put_object(Bucket=bucket_name, Key=f'{username}/')
        return {'statusCode': 200, 'body': f'Folder {username} created.'}


# outputs a list containing paths to images
def parse_request(event):
    paths = []
    folder = event['username']
    files = event['files']
    files_list = files.split(',')
    for element in files_list:
        paths.append(folder + "/" + element)
    return paths
    
def add_files(paths, event):
    successful_counter = 0
    counter = 0
    files_data = event['file_data'].split(",")
    for file_path, encoded_file in zip(paths, files_data):
        print(encoded_file)
        successful_counter += 1
        counter += 1
        decoded_file = base64.b64decode(encoded_file)
        try:
            response = s3.put_object(Body=decoded_file, Bucket=bucket_name, Key=file_path)
        except Exception:
            successful_counter -= 1
            break
    return successful_counter, counter
            
    

def lambda_handler(event, context):
    check_for_dir(event['username']) 

    paths = parse_request(event)
    if not check_compatibility(len(event['file_data'].split(",")), len(paths)):
        return False
    
    successful_uploads, counter = add_files(paths, event)
    return {
        'statusCode': 200,
        'body': str(successful_uploads) + '/' + str(counter) + ' Files uploaded successfully'
    }
