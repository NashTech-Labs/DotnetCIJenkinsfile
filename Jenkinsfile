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
        stage('Publish') {
            steps {
                dir('[ProjectName]/[ProjectName]') {
                    bat '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" /t:Publish /p:Configuration=Release /p:OutputPath=..\\publish'
                }
            }
        }
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