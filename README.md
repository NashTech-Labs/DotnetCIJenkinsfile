# Jenkins DotnetCI Template

This project template demonstrates a simple setup using MSBuild on Jenkins, with integration to Bitbucket and FTP deployment. The project structure is as follows:

## Directory Structure

```yaml
    DotnetCIJenkinsfile
    ├── [ProjectName]
    │   └── [ProjectName]
    │       ├── [ProjectName]
    │       │   ├── App.config
    │       │   ├── [ProjectName].csproj
    │       │   ├── Program.cs
    │       │   └── Properties
    │       │       └── AssemblyInfo.cs
    │       └── [ProjectName].sln
    ├── Jenkinsfile
    └── README.txt
```

* **Note**: Here [ProjectName] is *HelloCD*

* [ProjectName]/[ProjectName]/[ProjectName]/: Contains the main project files.
* [ProjectName]/[ProjectName]/[ProjectName]/Properties/: Contains the project properties.
* [ProjectName]/[ProjectName]/[ProjectName].sln: Solution file.
* [ProjectName]/[ProjectName]/[ProjectName].csproj: Project file.

## Prerequisites

* Jenkins server with the following plugins:
  * Git
  * Pipeline
* WindowsOS
* MSBuild (Visual Studio 2022 Community Edition)

## Setup

1. Configure the Source Code Manager (Bitbucket,Github, Gitlab) with your code to access the Jenkinsfile.
2. Ensure that MSBuild is installed and added to your system PATH.
3. Configure Jenkins with the required credentials for Source Code Manager and FTP.
4. Set up your Jenkins job to use the provided Jenkinsfile.

## Jenkins Pipeline Stages

1. Build: Compiles the solution using MSBuild.
2. Publish: Publishes the project to a specified output directory.
3. Deploy to FTP: Uploads the published executable to the FTP server.

## Jenkinsfile

The Jenkinsfile provided below is used to automate the build, publish, and deploy processes:

### Stage: Build

-----------------

```groovy
    pipeline {
        agent any
        tools {
            msbuild 'MSBuild_Default'
        }
        stages {
            stage('Build') {
                steps {
                    dir('[ProjectName]/[ProjectName]') {
                        bat '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" /t:Build [ProjectName].sln'
                    }
                }
            }
```

**Explanation**:
In the 'Build' stage, the project is compiled using MSBuild. The 'dir' step navigates to the project directory, and the 'bat' command runs MSBuild to compile the solution file (.sln).

### Stage: Publish

-----------------

```groovy
        stage('Publish') {
            steps {
                dir('[ProjectName]/[ProjectName]') {
                    bat '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" /t:Publish /p:Configuration=Release /p:OutputPath=..\\publish'
                }
            }
        }
```

**Explanation**:
In the 'Publish' stage, the compiled project is published to a specified output directory. The 'dir' step navigates to the project directory, and the 'bat' command runs MSBuild to publish the project in Release configuration to the output path.

### Stage: Deploy to FTP

-----------------

```groovy
        stage('Deploy to FTP') {
            environment {
                FTP_CREDENTIALS = credentials('FTP_CREDENTIALS')
            }
            steps {
                script {
                    withCredentials([usernamePassword(credentialsId: 'FTP_CREDENTIALS', usernameVariable: 'FTP_USERNAME', passwordVariable: 'FTP_PASSWORD')]) {
                        bat "curl -T C:\\tools\\Jenkins\\.jenkins\\workspace\\[JobName]\\[ProjectName]\\[ProjectName]\\publish\\[ProjectName].exe -u %FTP_USERNAME%:%FTP_PASSWORD% ftp://[FTP_Server_Address]/"
                    }
                }
            }
        }
    }
}
```

**Explanation**:
In the 'Deploy to FTP' stage, the published executable is uploaded to the FTP server. The 'environment' block sets up the FTP credentials. The 'withCredentials' step uses these credentials to authenticate the FTP upload. The 'bat' command runs a curl command to upload the file to the specified FTP server.

## Usage

1. Commit and push your changes to Bitbucket.
2. Trigger the Jenkins job manually or set up a webhook for automatic triggering.
3. Verify the build and deployment status in the Jenkins console output.

## Notes

* Ensure that the FTP server is properly configured to accept uploads.
* Update the FTP server address and credentials in the Jenkinsfile as required.
* Replace [ProjectName], [JobName], and [FTP_Server_Address] with your actual project name, Jenkins job name, and FTP server address respectively.
