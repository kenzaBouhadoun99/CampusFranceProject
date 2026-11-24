
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

        stage('Prepare Allure Demo') {
            steps {
                bat '''
                    if not exist allure-results mkdir allure-results
                    echo {"uuid":"demo-001","name":"Test de connexion","status":"passed","start":1763970000000,"stop":1763970001000,"stage":"finished"} > allure-results\\result-demo.json
                '''
            }
        }
    }

    post {
        always {
            allure([
                includeProperties: false,
                results: [[path: 'allure-results']]
            ])
        }
    }
}
