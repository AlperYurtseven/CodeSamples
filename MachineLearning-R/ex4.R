#Liubov Shilova - 7003130
#Alper Yurtseven - 2579122

#install.packages("tidyverse")
#install.packages("boot")
#install.packages("Matrix")
#install.packages("glmnet")
#install.packages("caret")
#install.packages("ggplot2")
#install.packages("pls")
#install.packages("leaps")
#install.packages("devtools")
#install.packages("ggfortify")
#install.packages("mdatools")

library(MASS)
library(ISLR)
library(boot)
library(glmnet)
library(tidyverse)
library(lattice)
library(pls)
library(leaps)
library(devtools)
library(usethis)
library(ggfortify)
library(mdatools)

prostate = load("prostate.Rdata")

prostate.whole.ds = rbind(prostate.train, prostate.test) 

data.full = data.frame(prostate.train)

regfit.full = regsubsets(lpsa ~ ., data = prostate.train, nvmax = 10)

reg.summary = summary(regfit.full)

par(mfrow = c(2, 2))

plot(reg.summary$cp, xlab = "Number of variables", ylab = "C_p", type = "l")
points(which.min(reg.summary$cp), reg.summary$cp[which.min(reg.summary$cp)], col = "red", cex = 2, pch = 20)
plot(reg.summary$bic, xlab = "Number of variables", ylab = "BIC", type = "l")
points(which.min(reg.summary$bic), reg.summary$bic[which.min(reg.summary$bic)], col = "red", cex = 2, pch = 20)
plot(reg.summary$adjr2, xlab = "Number of variables", ylab = "Adjusted R^2", type = "l")
points(which.max(reg.summary$adjr2), reg.summary$adjr2[which.max(reg.summary$adjr2)], col = "red", cex = 2, pch = 20)
plot(reg.summary$rsq, xlab = "Number of variables", ylab = "R^2", type = "l")
points(which.max(reg.summary$rsq), reg.summary$rsq[which.max(reg.summary$rsq)], col = "red", cex = 2, pch = 20)

coef(regfit.full, which.max(reg.summary$cp))

coef(regfit.full, which.max(reg.summary$bic))

coef(regfit.full, which.max(reg.summary$adjr2))

coef(regfit.full, which.max(reg.summary$rsq))

fit.lm = lm(lpsa~lcavol+lweight+svi+lcp+pgg45,data= prostate.train)

pred.lm.test = predict(fit.lm,prostate.test)

mean((pred.lm.test - prostate.test$lpsa)^2)

pred.lm.train = predict(fit.lm,prostate.train)

mean((pred.lm.train - prostate.train$lpsa)^2)

fit.pcr.test = pcr(lpsa ~ ., data = prostate.test, scale = TRUE, validation = "CV")
summary(fit.pcr.test)
validationplot(fit.pcr.test, val.type = "MSEP")

fit.pcr.train = pcr(lpsa ~ ., data = prostate.train, scale = TRUE, validation = "CV")
summary(fit.pcr.train)
validationplot(fit.pcr.train, val.type = "MSEP")

pred.pcr.test = predict(fit.pcr.train, prostate.test, ncomp = 8)
mean((pred.pcr.test - prostate.test$lpsa)^2)

pred.pcr.train = predict(fit.pcr.train, prostate.train, ncomp = 8)
mean((pred.pcr.train - prostate.train$lpsa)^2)

fit.pls.train = plsr(lpsa ~ ., data =prostate.train, scale = TRUE, validation = "CV")
validationplot(fit.pls.train, val.type = "MSEP")

fit.pls.test = plsr(lpsa ~ ., data =prostate.test, scale = TRUE, validation = "CV")
validationplot(fit.pls.test, val.type = "MSEP")

pred.pls.test = predict(fit.pls.train, prostate.test, ncomp = 8)
mean((pred.pls.test - prostate.test$lpsa)^2)
pred.pls.train = predict(fit.pls.train, prostate.train, ncomp = 8)
mean((pred.pls.train - prostate.train$lpsa)^2)

test.avg = mean(prostate.test$lpsa)
lm.r2 = 1 - mean((pred.lm.test - prostate.test$lpsa)^2) / mean((test.avg - prostate.test$lpsa)^2)
pcr.r2 = 1 - mean((pred.pcr.test - prostate.test$lpsa)^2) / mean((test.avg - prostate.test$lpsa)^2)
pls.r2 = 1 - mean((pred.pls.test - prostate.test$lpsa)^2) / mean((test.avg - prostate.test$lpsa)^2)

lm.r2
pcr.r2
pls.r2

prostate.whole.ds$threshold = factor(ifelse(prostate.whole.ds$lpsa > 2.5, "Yes", "No"))

prostate.pca = prcomp(prostate.whole.ds[,1:9], scale.=TRUE, rank. = 4)

prostate.pca

par(mfrow=c(1,2))
plot(prostate.pca$x[,c(1,2)],col= ifelse(prostate.whole.ds$threshold == "Yes", 'red','green'),pch=19,
     xlab="PC1",ylab="PC2", main="PC1 vs PC2")
plot(prostate.pca$x[,c(1,3)],col= ifelse(prostate.whole.ds$threshold == "Yes", 'red','green'),pch=19,
     xlab="PC1",ylab="PC3", main="PC1 vs PC3")
plot(prostate.pca$x[,c(1,4)],col= ifelse(prostate.whole.ds$threshold== "Yes", 'red','green'),pch=19,
     xlab="PC1",ylab="PC4", main="PC1 vs PC4")
plot(prostate.pca$x[,c(2,3)],col= ifelse(prostate.whole.ds$threshold == "Yes", 'red','green'),pch=19,
     xlab="PC3",ylab="PC4", main="PC2 vs PC3")
plot(prostate.pca$x[,c(2,4)],col= ifelse(prostate.whole.ds$threshold == "Yes", 'red','green'),pch=19,
     xlab="PC2",ylab="PC4", main="PC2 vs PC4")
plot(prostate.pca$x[,c(3,4)],col= ifelse(prostate.whole.ds$threshold == "Yes", 'red','green'),pch=19,
     xlab="PC3",ylab="PC4", main="PC3 vs PC4")

prostate.pls = pls(x = as.matrix(prostate.whole.ds[,1:8]), y = as.matrix(prostate.whole.ds[,9]),ncomp = 4, cv = 10)

prostate.pls$xloadings

par(mfrow = c(1, 2))
plotXScores(prostate.pls$calres,comp = c(1,2),main = "PLS1 vs PLS2", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
plotXScores(prostate.pls$calres,comp = c(1,3),main = "PLS1 vs PLS3", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
plotXScores(prostate.pls$calres,comp = c(1,4),main = "PLS1 vs PLS4", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
plotXScores(prostate.pls$calres,comp = c(2,3),main = "PLS2 vs PLS3", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
plotXScores(prostate.pls$calres,comp = c(2,4),main = "PLS2 vs PLS4", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
plotXScores(prostate.pls$calres,comp = c(3,4),main = "PLS3 vs PLS4", cgroup = as.factor(ifelse(prostate.whole.ds$lpsa > 2.5, ">2.5", "<2.5")))
