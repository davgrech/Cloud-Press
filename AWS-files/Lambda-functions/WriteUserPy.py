import boto3
import hashlib
import base64
import json
import os


def lambda_handler(event, context):
    # Get the username and password from the event
    username = event['username']
    password = event['password']


    # Create a DynamoDB client
    dynamodb = boto3.client('dynamodb')

    # Check if the user exists in the table
    response = dynamodb.get_item(
        TableName='users',
        Key={
            'username': {'S': username}
        }
    )

    if 'Item' in response:
        # User already exists
        return {'body' : "User already exist"}
    else:
        # User doesn't exist, add it to the table
        dynamodb.put_item(
            TableName='users',
            Item={
                'username': {'S': username},
                'password': {'S': password}
            }
        )
        lambda_client = boto3.client('lambda')
        
        with open("welcome.txt", 'rb') as f:
            file_data = f.read()
        
        # Encode the file contents as base64
        encoded_data = base64.b64encode(file_data)
        
        # Return the encoded data as a string
        file_data = encoded_data.decode('utf-8')
        response = lambda_client.invoke(
        FunctionName='LambdaAddS3',
        Payload=json.dumps({'username': username,
                            'files': "welcome.txt",
                            "file_data": file_data})
        ) 
        json_str = response['Payload'].read().decode()   
        upload_response = (json.loads(json_str))['body']
        
        # Parse the response from the target Lambda function
        print(upload_response)
        return {'body' : "Registration successful"}
