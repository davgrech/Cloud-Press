# AWS Environment Overview

Welcome to the README for our AWS environment. This document provides a comprehensive overview of the files and directories within our AWS setup, showcasing how different components interact to provide seamless account authentication, user file management, and web application hosting.

## Lambda Functions

The `lambda_functions` directory holds Lambda functions that play a critical role in orchestrating interactions between various services:

- **Authentication:** `GetUserPy.py` checks if inputted user exists in the dynamoDB database. `WriteUserPy.py` adds users to the dynamoDB database, used in user registration.

- **File Management:** Responsible for managing file operations between the EC2 instances and the S3 bucket. 
`LambdaListAllFiles.py` lists all files under the given username. 

`LambdaAddS3.py` adds given files to the S3 bucket (cpfileuploads) under the inputted username `cpfileuploads/<username>`.

`LambdaFetchS3.py` fetches a file from the `cpfileuploads/<username>` folder, and sends it to the ec2 instance.

## Web Application

The `web_app` directory contains the Flask web application:

- **User Authentication:** provides an interface for user registration and login, with a pretty good looking UI ;)

- **File Management:** using the lambda functions, users can upload and retrieve files from the S3 bucket. The app acts as a bridge between user interactions and backend services.

## EC2 Instances

Inside the `ec2_instances` directory, there is a snapshot of our EC2 instances:

- **Snapshot Preservation:** Regular snapshots capture the complete state of our EC2 instances. This includes configurations, installed software, and file systems. Snapshots serve as reliable restore points in case of unforeseen issues.

