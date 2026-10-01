pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore PulsakuService.csproj'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build PulsakuService.csproj --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --configuration Release --no-build'
            }
        }
    }
}