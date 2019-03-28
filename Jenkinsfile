pipeline {
  agent any
  
  def buildURL = ${BUILD_URL}
  def newBuildURL = buildURL.replace("job/${env.JOB_NAME}", "blue/organizations/jenkins/${env.JOB_NAME}/detail/${env.JOB_NAME}")

  stages {
    stage('Test') {
      steps {
        sh '''echo ${BUILD_URL}
        
        echo Thinking to change

echo ${newBuildUrl}'''
      }
    }
  }
} 
