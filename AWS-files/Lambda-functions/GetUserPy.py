import boto3

def lambda_handler(event, context):
    dynamodb = boto3.resource('dynamodb')
    table = dynamodb.Table('users')
    
    username = event['username']
    password = event['password']
    
    response = table.get_item(
        Key={
            'username': username
        }
    )
    
    
    db_response = (response.get('Item'))

    
    # Check if returned data is invalid
    if not db_response:
        return False
    
        
    # return true if data is valid
    if db_response['password'] == event['password']:
        return True
    return False