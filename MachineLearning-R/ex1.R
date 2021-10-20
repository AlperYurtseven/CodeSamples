library(MASS)
library(ISLR)

ozone_data = ozone$ozone
radiation_data = ozone$radiation
temperature_data = ozone$temperature
wind_data = ozone$wind

plot(ozone_data, radiation_data, main = "Ozone vs Radiation",
     xlab = "Ozone", ylab = "Radiation",
     pch = 19, frame = TRUE)
abline(lm(radiation_data ~ ozone_data, data = ozone), col = "blue")
lines(lowess(ozone_data, radiation_data), col = "red")
cor(ozone_data,radiation_data)

plot(ozone_data, temperature_data, main = "Ozone vs Temperature",
     xlab = "Ozone", ylab = "Temperature",
     pch = 19, frame = FALSE, xlim=c(0,150), ylim=c(40,100))
abline(lm(temperature_data ~ ozone_data, data = ozone), col = "blue")
lines(lowess(ozone_data, temperature_data), col = "red")
cor(ozone_data,temperature_data)

plot(ozone_data, wind_data, main = "Ozone vs Wind",
     xlab = "Ozone", ylab = "Wind",
     pch = 19, frame = FALSE)
abline(lm(wind_data ~ ozone_data, data = ozone), col = "blue")
lines(lowess(ozone_data, wind_data), col = "red")
cor(ozone_data,wind_data)

plot(radiation_data, temperature_data, main = "Radiation vs Temperature",
     xlab = "Radiation", ylab = "Temperature",
     pch = 19, frame = FALSE)
abline(lm(temperature_data ~ radiation_data, data = ozone), col = "blue")
lines(lowess(radiation_data, temperature_data), col = "red")
cor(radiation_data,temperature_data)

plot(radiation_data, wind_data, main = "Radiation vs Wind",
     xlab = "Radiation", ylab = "Wind",
     pch = 19, frame = FALSE)
abline(lm(wind_data ~ radiation_data, data = ozone), col = "blue")
lines(lowess(radiation_data, wind_data), col = "red")
cor(radiation_data,wind_data)

plot(temperature_data, wind_data, main = "Temperature vs Wind",
     xlab = "Temperature", ylab = "Wind",
     pch = 19, frame = FALSE)
abline(lm(wind_data ~ temperature_data, data = ozone), col = "blue")
lines(lowess(temperature_data, wind_data), col = "red")
cor(temperature_data,wind_data)

lm.fit = lm(ozone~. , data=ozone)
summary(lm.fit)

lm(formula = ozone~., data=ozone)

names = names(ozone)

vector = c()

for(i in names){
        for(j in names){
                if(i!=j){
                        print(c(i, j, cor(ozone[i],ozone[j])))
                }
        }
}

mpg_data = Auto$mpg
cylinders_data = Auto$cylinders
displacement_data = Auto$displacement
horsepower_data = Auto$horsepower
year_data = Auto$year

plot(mpg_data, cylinders_data, main = "mpg vs cylinders",
     xlab = "mpg", ylab = "cylinders",
     pch = 19, frame = FALSE)
abline(lm(cylinders_data ~ mpg_data, data = ozone), col = "blue")
lines(lowess(mpg_data, cylinders_data), col = "red")
cor(mpg_data,cylinders_data)

plot(mpg_data, displacement_data, main = "mpg vs displacement",
     xlab = "mpg", ylab = "displacements",
     pch = 19, frame = FALSE)
abline(lm(displacement_data ~ mpg_data, data = ozone), col = "blue")
lines(lowess(mpg_data, displacement_data), col = "red")
cor(mpg_data,displacement_data)

plot(mpg_data, horsepower_data, main = "mpg vs horsepower",
     xlab = "mpg", ylab = "horsepower",
     pch = 19, frame = FALSE)
abline(lm(horsepower_data ~ mpg_data, data = ozone), col = "blue")
lines(lowess(mpg_data, horsepower_data), col = "red")
cor(mpg_data,horsepower_data)

plot(mpg_data, year_data, main = "mpg vs year",
     xlab = "mpg", ylab = "year",
     pch = 19, frame = FALSE)
abline(lm(year_data ~ mpg_data, data = ozone), col = "blue")
lines(lowess(mpg_data, year_data), col = "red")
cor(mpg_data,year_data)


mpgvscylinders= lm(formula = mpg~cylinders, data=Auto)

summary(mpgvscylinders)$r.squared


mpgvsdisplacement= lm(formula = mpg~displacement, data=Auto)

summary(mpgvsdisplacement)$r.squared


mpgvshorsepower= lm(formula = mpg~horsepower, data=Auto)

summary(mpgvshorsepower)$r.squared


mpgvsyear= lm(formula = mpg~year, data=Auto)

summary(mpgvsyear)$r.squared


#x = lm(formula = mpg~ . - name, data=Auto)

#plot(x)

#lm(formula = mpg~ . - name, data=Auto)

summary(lm(mpg~ . - name, data=Auto))

#summary(lm(formula = mpg~ . - name, data=Auto))$r.squared

#x = lm(formula = mpg~ . - name, data=Auto)

plot(lm(mpg~ . - name, data=Auto))

cylinders_displacement = lm(cylinders~log(displacement), data=Auto)
        
weight_displacement = lm(weight~sqrt(displacement), data=Auto)

year_displacement = lm(year~(displacement^2), data=Auto)

summary(cylinders_displacement)

summary(weight_displacement)

summary(year_displacement)




