library(ISLR)
library(MASS)

#install.packages("ggplot2")
#Please run the code above if needed, I preferred to use ggplot instead of just plot just to get a better image
#however, If it is not permitted, I also used plot function too as stated in the assignment sheet.
#but only plot image was not good :( .

phoneme = read.csv("phoneme.csv",header = TRUE)

phoneme$speaker = as.character(phoneme$speaker)

test_or_train = vector()

for (i in 1:nrow(phoneme)){
  if(strsplit(phoneme[i,259],"[.]")[[1]][1] == "test"){
    test_or_train[i] = "test"
  }else{
    test_or_train[i] = "train"
  }
}

phoneme$type = test_or_train

test = subset(phoneme, type == "test")
train = subset(phoneme, type == "train")

drops = c("row.names","speaker","g","type")

phoneme_dropped = phoneme[, !(names(phoneme) %in% drops)]
test.set = test[, !(names(test) %in% drops)]
train.set = train[, !(names(train) %in% drops)]

train.response = subset(train)$g
test.response = subset(test)$g

lda.model = lda(train.set, grouping = train.response)

mean = colSums(lda.model$prior * lda.model$means)
train.mod = scale(train.set, center = mean, scale = FALSE) %*% 
  lda.model$scaling
lda.prediction.train = predict(lda.model, train.set)
lda.prediction.test = predict(lda.model, test.set)

test_acc = length(which(lda.prediction.test$class == test.response))/length(test.response)
print(c("Test LDA Accuracy: " , test_acc))

train_acc = length(which(lda.prediction.train$class == train.response))/length(train.response)
print(c("Train LDA Accuracy: " , train_acc))

print(c("Test LDA Error: " , 1-test_acc))
print(c("Train LDA Error: " , 1-train_acc))

plot.lda = data.frame(train.mod, "Phonemes" = train.response)
library(ggplot2)
ggplot(plot.lda, aes(x = LD1, y = LD2, color = Phonemes)) + geom_point()

plot(lda.model, dimen = 2, main = "LDA plot of Test dataset")

aa_ao_test = subset(test, g == "ao" | g == "aa")
aa_ao_train = subset(train, g == "ao"| g == "aa")

aa_ao_train.response = subset(aa_ao_train)$g
aa_ao_test.response = subset(aa_ao_test)$g

aa_ao_test.set = aa_ao_test[, !(names(aa_ao_test) %in% drops)]
aa_ao_train.set = aa_ao_train[, !(names(aa_ao_train) %in% drops)]

aa_ao_lda.model = lda(aa_ao_train.set, grouping = aa_ao_train.response)

mean = colSums(aa_ao_lda.model$prior * aa_ao_lda.model$means)
aa_ao_train.mod = scale(aa_ao_train.set, center = mean, scale = FALSE) %*% 
  aa_ao_lda.model$scaling
aa_ao_lda.prediction.train = predict(aa_ao_lda.model, aa_ao_train.set)
aa_ao_lda.prediction.test = predict(aa_ao_lda.model, aa_ao_test.set)

aa_ao_lda_test_acc = length(which(aa_ao_lda.prediction.test$class == aa_ao_test.response))/length(aa_ao_test.response)
print(c("AA & AO LDA Test Accuracy: " , aa_ao_lda_test_acc))

aa_ao_lda_train_acc = length(which(aa_ao_lda.prediction.train$class == aa_ao_train.response))/length(aa_ao_train.response)
print(c("AA & AO LDA Train Accuracy: " , aa_ao_lda_train_acc))

print(c("AA & AO LDA Test Error: " , 1-aa_ao_lda_test_acc))
print(c("AA & AO LDA Train Error: " , 1-aa_ao_lda_train_acc))

qda.model = qda(train.set, grouping = train.response)
qda.prediction.test = predict(qda.model, test.set)
qda.prediction.train = predict(qda.model, train.set)

qda_test_acc = length(which(qda.prediction.test$class == test.response))/length(test.response)
print(c("QDA Test Accuracy: " , qda_test_acc))
print(c("QDA Test Error: " , 1-qda_test_acc))

qda_train_acc = length(which(qda.prediction.train$class == train.response))/length(train.response)
print(c("QDA Train Accuracy: " , qda_train_acc))
print(c("QDA Train Error: " , 1-qda_train_acc))

aa_ao_train$lda = aa_ao_lda.prediction.train$class
table(aa_ao_train$lda,aa_ao_train$g)

aa_ao_test$lda = aa_ao_lda.prediction.test$class
table(aa_ao_test$lda,aa_ao_test$g)

aa_ao_qda.model = qda(aa_ao_train.set, grouping = aa_ao_train.response)
aa_ao_qda.prediction = predict(aa_ao_qda.model, aa_ao_test.set)
aa_ao_qda_acc = length(which(aa_ao_qda.prediction$class == aa_ao_test.response))/length(aa_ao_test.response)
#print(c("AA & AO QDA Accuracy: " , aa_ao_qda_acc))

aa_ao_qda.prediction.train = predict(aa_ao_qda.model, aa_ao_train.set)
aa_ao_qda.prediction.test = predict(aa_ao_qda.model, aa_ao_test.set)

aa_ao_qda_test_acc = length(which(aa_ao_qda.prediction.test$class == aa_ao_test.response))/length(aa_ao_test.response)
print(c("AA & AO QDA Test Accuracy: " , aa_ao_qda_test_acc))

aa_ao_qda_train_acc = length(which(aa_ao_qda.prediction.train$class == aa_ao_train.response))/length(aa_ao_train.response)
print(c("AA & AO QDA Train Accuracy: " , aa_ao_qda_train_acc))

print(c("AA & AO QDA Test Error: " , 1-aa_ao_qda_test_acc))
print(c("AA & AO QDA Train Error: " , 1-aa_ao_qda_train_acc))

aa_ao_train$qda = aa_ao_qda.prediction.train$class
table(aa_ao_train$qda,aa_ao_train$g)

aa_ao_test$qda = aa_ao_qda.prediction.test$class
table(aa_ao_test$qda,aa_ao_test$g)

