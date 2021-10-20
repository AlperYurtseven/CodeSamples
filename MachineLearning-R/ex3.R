#Liubov Shilova - 7003130
#Alper Yurtseven - 2579122

install.packages("tidyverse")
install.packages("boot")
install.packages("Matrix")
install.packages("glmnet")
install.packages("caret")
install.packages("ggplot2")

library(MASS)
library(ISLR)
library(boot)
#library(Matrix)
library(glmnet)
library(ggplot2)
library(tidyverse)
library(caret)
library(lattice)

prostate = read.table("prostate.txt")

for (col in 1:(ncol(prostate)-1)) {
  prostate[,col] = c(scale(prostate[,col]))
}

test = subset(prostate, train == "FALSE")
train = subset(prostate, train == "TRUE")

test = test[,!names(test) %in% "train"]
rownames(test) = c()
train = train[,!names(train) %in% "train"]
rownames(train) = c()

prostate = prostate[,!names(prostate) %in% "train"]

#LOOCV

train.control = trainControl(method = "LOOCV")

model <- train(lpsa ~., data = prostate, method = "lm",
               trControl = train.control)

print(model)

#LOOCV suggested in ISLR

loocv.glm.fit=glm(lpsa???. ,data=train)
loocv.cv.err=cv.glm(train ,loocv.glm.fit)

sqrt(min(loocv.cv.err$delta))


# 5-fold cross validation 

set.seed(35) 
fivefold.train.control = trainControl(method = "cv", number = 5)

fivefold.model = train(lpsa ~., data = prostate, method = "lm",
                       trControl = fivefold.train.control)

print(fivefold.model)

#5-fold cv suggested in ISLR

fivefold.cv.error=rep(0,5)
for (i in 1:5){
  fivefold.glm.fit=glm(lpsa???poly(lcavol+lweight+age+lbph+svi+lcp+gleason+pgg45 ,i),data=train)
  fivefold.cv.error[i]=cv.glm(train ,fivefold.glm.fit)$delta [1]
}

sqrt(min(fivefold.cv.error))

# 10-fold cross validation 

set.seed(35) 
tenfold.train.control = trainControl(method = "cv", number = 10)

tenfold.model = train(lpsa ~., data = prostate, method = "lm",
                      trControl = tenfold.train.control)

print(tenfold.model)

#10-fold cv suggested in ISLR

tenfold.cv.error=rep(0,10)
for (i in 1:10){
  tenfold.glm.fit=glm(lpsa???poly(lcavol+lweight+age+lbph+svi+lcp+gleason+pgg45 ,i),data=train)
  tenfold.cv.error[i]=cv.glm(train ,tenfold.glm.fit)$delta [1]
}

sqrt(min(tenfold.cv.error))

#Linear reg model

linearModel = glm(lpsa ~ ., data=train)

pred = predict(linearModel,test)
mse = (mean((test$lpsa - pred)^2))

rmse = sqrt(mse)

rmse

xtrain=model.matrix (lpsa~.,train)[,-1]
ytrain=train$lpsa
xtest=model.matrix (lpsa~.,test)[,-1]
ytest=test$lpsa

set.seed (35)
cv.out=glmnet(xtrain,ytrain,alpha =0)
plot(cv.out, xvar="lambda")

bestlam=min(cv.out$lambda)
bestlam

model =cv.glmnet(xtrain,ytrain,alpha=0,nfolds=10)

ridge.coef=predict(cv.out ,type="coefficients",s= bestlam) [1:9,]
ridge.coef

pred=predict(model,s=bestlam ,newx=xtest)
MSE=mean((pred-ytest)^2)
print(MSE)

pred=predict(model,s=bestlam ,newx=xtrain)
MSE=mean((pred-ytrain)^2)
print(MSE)

set.seed (35)
cv.out=glmnet(xtrain,ytrain,alpha =1)
plot(cv.out, xvar="lambda")

bestlam=min(cv.out$lambda)
bestlam

model =cv.glmnet(xtrain,ytrain,alpha=1,nfolds=10)

pred=predict(model,s=bestlam ,newx=xtest)
MSE=mean((pred-ytest)^2)
print(MSE)

pred=predict(model,s=bestlam ,newx=xtrain)
MSE=mean((pred-ytrain)^2)
print(MSE)

out=glmnet(xtrain,ytrain,alpha=1)
lasso.coef=predict (out ,type="coefficients",s= bestlam) [1:9,]
lasso.coef

