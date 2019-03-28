def buildURL = ${env.BUILD_URL}
def newBuildURL = buildURL.replace("job/${env.JOB_NAME}", "blue/organizations/jenkins/${env.JOB_NAME}/detail/${env.JOB_NAME}")

pipeline {
  agent any
  stages {
    stage('Test') {
      steps {
        sh '''

echo ${newBuildURL}'''
      }
    }
  }
}
