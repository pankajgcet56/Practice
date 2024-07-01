#!/usr/bin/env bash

# --------------------for LOOPS-------------------

# $@ is array of Inputs get from user
NAMES=$@ 

for NAME in $NAMES
do
    if [ $NAME = "Ppapa" ]
    then
    break # Make Exit
    fi

    if [ $NAME = "Papa" ]
    then
    continue # Make pointer on top of For loop
    fi

    echo "Hello $NAME"
done

echo "For Loop Done"
exit 0

# --------------------if then else elif-------------------

COLOR=$1

if [ $COLOR = "blue" ]
then
echo "Color is blue"
else 
    echo "Color is Not Blue"
fi

TEMP_VAL=50
USER_GUESS=$2

# -eq   => if equal
# -ne   => if not equal
# -lt   => if less than
# -gt   => if greater than
# -le   => if less than or equal
# -ge   => if greater than or equal

if [ $USER_GUESS -lt $TEMP_VAL ]
then
    echo "Less Than"
elif [ $USER_GUESS -gt $TEMP_VAL ]
then
    echo "Greater Than"

else
    echo "equal or to high"
fi


# --------------------while LOOPS-------------------

COUNT=0

while [ $COUNT -lt 10 ]
do
    echo "COUNT = $COUNT"
    ((COUNT++))
done

echo "While Finished"

