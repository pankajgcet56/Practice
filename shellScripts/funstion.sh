#!/usr/bin/env bash

# ------------------     Pipe    ------------------

FILES=`ls -l`
# FILES=`ls -la | sort -r | head -3`

COUNT=1

for FILE in $FILES
do
    echo "File #$COUNT = $FILE"
    ((COUNT++))
done
exit 0
# ------------------     Function    ------------------

function Hello() {
    local LNAME=$1
    echo "Hello $LNAME"
}

Goodbye() {
    echo "Good Baye! $1"
}

echo "Calling Hello Func"
Hello Pankaj

echo "Calling Good Baye"
Goodbye Rajan

exit 0