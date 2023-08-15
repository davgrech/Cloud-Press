import boto3

def lambda_handler(event, context):
    # Get the S3 bucket name from environment variables or elsewhere
    s3_bucket = 'cpfileuploads'
    
    # Get the path from the event
    path = event['username']
    
    # Create an S3 client
    s3_client = boto3.client('s3')
    
    # List objects in the specified path
    objects = s3_client.list_objects_v2(Bucket=s3_bucket, Prefix=path)['Contents']
    
    # Extract file names from the objects
    filenames = [obj['Key'] for obj in objects if obj['Key'] != path]
    filenames = [filename.split('/')[-1] for filename in filenames if '/' in filename]
    del filenames[0]
    return {'statusCode': 200, 'body': filenames}
