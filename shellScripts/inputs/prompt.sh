#!/usr/bin/env bash

# Validations

VALID=0

while [ $VALID -eq 0 ]
do
    read -p "Please Enter your name & Age : " NAME1 AGE
    if [[ ( -z $NAME1 ) || (-z $AGE ) ]]
    then
        echo "Not Enough Param Is Passed"
        continue
    elif [[ ! $NAME1 =~ ^[a-zA-Z]+$ ]]
    then
        echo "Non Alpha char passed"
        continue
    elif [[ ! $AGE =~ ^[0-9]+$ ]]
    then
        echo "Non Digit char is detected"
        continue
    fi
    VALID=1
done

echo "Name = $NAME1 Age = $AGE"
exit 0


read -p "what is Your Name: " NAME
echo Your Name is $NAME
exit 0