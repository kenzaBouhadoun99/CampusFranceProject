pipeline {
    agent any

    stages {

        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }

        stage('Tests') {
    steps {
        bat 'dotnet test --no-build'
            }
}
    }
}