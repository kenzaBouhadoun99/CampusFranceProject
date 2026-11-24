
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
                catchError(buildResult: 'UNSTABLE', stageResult: 'FAILURE') {
                    bat 'dotnet test --no-build'
                }
            }
        }

        stage('Prepare Allure Demo') {
            steps {
                bat '''
                    if not exist allure-results mkdir allure-results

                    powershell -NoProfile -Command "$r = @{uuid='demo-001'; name='Test de connexion'; status='passed'; start=1763970000000; stop=1763970001000; stage='finished'} | ConvertTo-Json; Set-Content -Path 'allure-results/result-demo.json' -Value $r -Encoding utf8"

                    dir allure-results
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
