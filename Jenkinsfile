pipeline {
  
    agent any
  
    environment {
        AWS_ACCOUNT_ID="842216378405"
        KUBECONFIG = '/var/lib/jenkins/.kube/prod-cluster'
        AWS_DEFAULT_REGION="eu-central-1"
        IMAGE_REPO_NAME="pil_fantasy_data"
        IMAGE_TAG="prod"
        REPOSITORY_URI = "${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_DEFAULT_REGION}.amazonaws.com/${IMAGE_REPO_NAME}"
    }
   
   
  stages {

    ///// Logging into AWS ECR 
    stage('ECR Logging') {
      steps {
        script {
          sh "aws ecr get-login-password --region ${AWS_DEFAULT_REGION}  | docker login --username AWS --password-stdin ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_DEFAULT_REGION}.amazonaws.com"
        }         
      }
    }
        
          
    //// Building Docker images
    stage('Building image') {
      steps{
        dir('PIL_Fantasy_Data_Integration.API'){
          script {
            dockerImage = docker.build "${IMAGE_REPO_NAME}:${IMAGE_TAG}"
          }
        }
      }
    }
   
    /// Uploading Docker images into AWS ECR
    stage('Pushing to ECR') {
     steps{  
       script {
         sh "docker tag ${IMAGE_REPO_NAME}:${IMAGE_TAG} ${REPOSITORY_URI}:$IMAGE_TAG"
         sh "docker push ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_DEFAULT_REGION}.amazonaws.com/${IMAGE_REPO_NAME}:${IMAGE_TAG}"
       }
     }
    }


    /// 
    stage('EKS Deployment') {
      steps{
        script {
          sh "kubectl --kubeconfig=${KUBECONFIG} apply -f kubernetes"
          sh "kubectl --kubeconfig=${KUBECONFIG} delete -f kubernetes"
          sh "kubectl --kubeconfig=${KUBECONFIG} apply -f kubernetes"
        }
      }  
    }
     
  }
}

