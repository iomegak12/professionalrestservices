pipeline {
  agent any
 
  stages {
    def buildURL = ${BUILD_URL}
    def newBuildURL = buildURL.replace("job/${env.JOB_NAME}", "blue/organizations/jenkins/${env.JOB_NAME}/detail/${env.JOB_NAME}")

    
    stage('Test') {
      steps {
        sh '''echo ${newBuildUrl}'''
      }
    }
  }
} 
