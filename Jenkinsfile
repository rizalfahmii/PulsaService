pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Test Jenkins') {
            steps {
                echo 'Jenkins berhasil mengambil project Pulsaku!'
            }
        }
    }
}