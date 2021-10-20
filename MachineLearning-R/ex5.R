#Liubov Shilova - 7003130
#Alper Yurtseven - 2579122

#install.packages("randomForest")
#install.packages("MASS")
#install.packages("caret")
#install.packages("ggplot2")

library(randomForest)
library(MASS)
library(caret)
library(ggplot2)
library(e1071)
library(ROCR)

breast_cancer_data = read.table("wdbc.data",sep = ",")
breast_cancer_data = breast_cancer_data[-1]

#shuffle the data

set.seed(35)

shuffled_breast_cancer_data = breast_cancer_data[sample(nrow(breast_cancer_data)),]
rownames(shuffled_breast_cancer_data) = NULL

normalization = function(x){ 
  return((x-min(x))/(max(x)-min(x)))
}

#normalization of columns from 3 to 32 (2 to 31 since we deleted first one)

normalized_breast_cancer_data = as.data.frame(lapply(shuffled_breast_cancer_data[2:31], normalization))
V2 = as.data.frame(shuffled_breast_cancer_data$V2,label = diag)

normalized_breast_cancer_data1 = as.data.frame(c(V2,normalized_breast_cancer_data))

#creating training and test data sets

training_shuffled_breast_cancer_data = normalized_breast_cancer_data1[1:400,]
names(training_shuffled_breast_cancer_data)[names(training_shuffled_breast_cancer_data) == "shuffled_breast_cancer_data.V2"] = "V2"
rownames(training_shuffled_breast_cancer_data) = NULL
test_shuffled_breast_cancer_data = normalized_breast_cancer_data1[400:nrow(normalized_breast_cancer_data1),]
names(test_shuffled_breast_cancer_data)[names(test_shuffled_breast_cancer_data) == "shuffled_breast_cancer_data.V2"] = "V2"
rownames(test_shuffled_breast_cancer_data) = NULL

training_shuffled_breast_cancer_data$V2 = factor(training_shuffled_breast_cancer_data$V2)

wald = randomForest(training_shuffled_breast_cancer_data$V2~., data = training_shuffled_breast_cancer_data)

wald

varImpPlot(wald)

training_responses = as.factor(training_shuffled_breast_cancer_data$V2)

train_pred = predict(wald, training_shuffled_breast_cancer_data)

confusionMatrix(train_pred, training_responses)

test_responses = as.factor(test_shuffled_breast_cancer_data$V2)

test_pred = predict(wald, test_shuffled_breast_cancer_data)

confusionMatrix(test_pred, test_responses)

a = vector()

for (i in 1:nrow(training_shuffled_breast_cancer_data)) {
  
  if(training_shuffled_breast_cancer_data[i,1] == 'B'){
    a = c(a,1)
  } 
  else{
    a = c(a,-1)
  }
  
}

b = vector()

for (i in 1:nrow(test_shuffled_breast_cancer_data)) {
  
  if(test_shuffled_breast_cancer_data[i,1] == 'B'){
    b = c(b,1)
  } 
  else{
    b = c(b,-1)
  }
  
}

training_shuffled_breast_cancer_data$V2 = as.factor(a)
test_shuffled_breast_cancer_data$V2 = as.factor(b)

svm.fit_training_radial = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=c(0.1,1,2,5,10))

tune.out_training_radial = tune(svm,factor(V2) ~.,data = training_shuffled_breast_cancer_data,kernel="radial", gamma=c(0.1,1,2,5,10),tunecontrol=tune.control(cross=5))
summary(tune.out_training_radial)

svm.fit_test_radial = svm(factor(V2) ~ . , data = test_shuffled_breast_cancer_data ,kernel="radial" , gamma=c(0.1,1,2,5,10))

tune.out_test_radial = tune(svm,factor(V2) ~.,data = test_shuffled_breast_cancer_data,kernel="radial", gamma=c(0.1,1,2,5,10),tunecontrol=tune.control(cross=5))
summary(tune.out_test_radial)

table(true=test_shuffled_breast_cancer_data$V2, pred=predict(tune.out_training_radial$best.model,newdata =test_shuffled_breast_cancer_data))

svm.fit_training_polynomial = svm(V2 ~ ., data = training_shuffled_breast_cancer_data, kernel = "polynomial", degree=c(1, 2, 5, 10))
#summary(svm.fit)

tune.out_training_polynomial = tune(svm,factor(V2) ~.,data = training_shuffled_breast_cancer_data,kernel="polynomial", degree=c(1, 2, 5, 10),tunecontrol=tune.control(cross=5))
summary(tune.out_training_polynomial)

svm.fit_test_polynomial = svm(V2 ~ ., data = training_shuffled_breast_cancer_data, kernel = "polynomial", degree=c(1, 2, 5, 10))
#summary(svm.fit)

tune.out_test_polynomial = tune(svm,factor(V2) ~.,data = test_shuffled_breast_cancer_data,kernel="polynomial", degree=c(1, 2, 5, 10),tunecontrol=tune.control(cross=5))
summary(tune.out_test_polynomial)

table(true=test_shuffled_breast_cancer_data$V2, pred=predict(tune.out_training_polynomial$best.model,newdata =test_shuffled_breast_cancer_data))

#radial

#fitted = attributes(predict(svm.fit_training_radial,training_shuffled_breast_cancer_data,decision.values = TRUE))

#fitted_test = attributes(predict(svm.fit_test_radial,test_shuffled_breast_cancer_data,decision.values = TRUE))

rocplot =function (pred , truth , ...){
  predob = prediction (pred , truth)
  perf = performance (predob , "tpr", "fpr")
  plot(perf ,...)
}

svm.fit_training_radial_01 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=0.1)
fitted_01 = attributes(predict(svm.fit_training_radial_01,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_01_test = attributes(predict(svm.fit_training_radial_01,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_radial_1 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=1)
fitted_1 = attributes(predict(svm.fit_training_radial_1,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_1_test = attributes(predict(svm.fit_training_radial_1,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_radial_2 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=2)
fitted_2 = attributes(predict(svm.fit_training_radial_2,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_2_test = attributes(predict(svm.fit_training_radial_2,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_radial_5 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=5)
fitted_5 = attributes(predict(svm.fit_training_radial_5,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_5_test = attributes(predict(svm.fit_training_radial_5,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_radial_10 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="radial" , gamma=10)
fitted_10 = attributes(predict(svm.fit_training_radial_10,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_10_test = attributes(predict(svm.fit_training_radial_10,test_shuffled_breast_cancer_data,decision.values = TRUE))

#par(mfrow=c(1,2))

#rocplot(fitted_01$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with gamma 0.1")
#rocplot(fitted_1$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with gamma 1",col="blue")
#rocplot(fitted_2$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with gamma 2",col="green")
#rocplot(fitted_5$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with gamma 5",col="red")
#rocplot(fitted_10$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with gamma 10",col="gold")

#rocplot(fitted_01_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma 0.1")
#rocplot(fitted_1_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma 1",col="blue")
#rocplot(fitted_2_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma 2",col="green")
#rocplot(fitted_5_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma 5",col="red")
#rocplot(fitted_10_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma 10",col="gold")

#rocplot(fitted_01_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with gamma=c(0.1,1,2,5,10)")
#rocplot(fitted_1_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="blue")
#rocplot(fitted_2_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="green")
#rocplot(fitted_5_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="red")
#rocplot(fitted_10_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="gold")

#polynomial degree=c(1, 2, 5, 10)

#fitted = attributes(predict(svm.fit_training_radial,training_shuffled_breast_cancer_data,decision.values = TRUE))

#fitted_test = attributes(predict(svm.fit_test_radial,test_shuffled_breast_cancer_data,decision.values = TRUE))


svm.fit_training_polynomial_1 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="polynomial" , degree=1)
fitted_1_polynomial = attributes(predict(svm.fit_training_polynomial_1,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_1_polynomial_test = attributes(predict(svm.fit_training_polynomial_1,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_polynomial_2 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="polynomial" , degree=2)
fitted_2_polynomial = attributes(predict(svm.fit_training_polynomial_2,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_2_polynomial_test = attributes(predict(svm.fit_training_polynomial_2,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_polynomial_5 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="polynomial" , degree=5)
fitted_5_polynomial = attributes(predict(svm.fit_training_polynomial_5,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_5_polynomial_test = attributes(predict(svm.fit_training_polynomial_5,test_shuffled_breast_cancer_data,decision.values = TRUE))

svm.fit_training_polynomial_10 = svm(factor(V2) ~ . , data = training_shuffled_breast_cancer_data ,kernel="polynomial" , degree=10)
fitted_10_polynomial = attributes(predict(svm.fit_training_polynomial_10,training_shuffled_breast_cancer_data,decision.values = TRUE))
fitted_10_polynomial_test = attributes(predict(svm.fit_training_polynomial_10,test_shuffled_breast_cancer_data,decision.values = TRUE))

#par(mfrow=c(1,2))

#rocplot(fitted_1_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with degree 1",col="blue")
#rocplot(fitted_2_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with degree 2",col="green")
#rocplot(fitted_5_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with degree 5",col="red")
#rocplot(fitted_10_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data with degree 10",col="gold")

#rocplot(fitted_1_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with degree 1",col="blue")
#rocplot(fitted_2_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with degree 2",col="green")
#rocplot(fitted_5_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with degree 5",col="red")
#rocplot(fitted_10_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with degree 10",col="gold")


#rocplot(fitted_1_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data with degree c(1, 2, 5, 10)")
#rocplot(fitted_2_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="green")
#rocplot(fitted_5_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="red")
#rocplot(fitted_10_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="gold")

#2 plots for 1 training 1 test combined

#Training Plots

rocplot(fitted_01$decision.values,training_shuffled_breast_cancer_data$V2,main="Training Data ROC curves")
rocplot(fitted_1$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="blue")
rocplot(fitted_2$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="royalblue")
rocplot(fitted_5$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="steelblue")
rocplot(fitted_10$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="turquoise")

rocplot(fitted_1_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="red")
rocplot(fitted_2_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="plum")
rocplot(fitted_5_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="hotpink")
rocplot(fitted_10_polynomial$decision.values,training_shuffled_breast_cancer_data$V2,add=T,col="maroon")

#2 plots for 1 training 1 test combined

#Test Plots

rocplot(fitted_01_test$decision.values,test_shuffled_breast_cancer_data$V2,main="Test Data ROC curves")
rocplot(fitted_1_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="blue")
rocplot(fitted_2_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="royalblue")
rocplot(fitted_5_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="steelblue")
rocplot(fitted_10_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="turquoise")

rocplot(fitted_1_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="red")
rocplot(fitted_2_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="plum")
rocplot(fitted_5_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="hotpink")
rocplot(fitted_10_polynomial_test$decision.values,test_shuffled_breast_cancer_data$V2,add=T,col="maroon")

