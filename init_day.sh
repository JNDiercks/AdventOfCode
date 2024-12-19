#!/bin/bash

if [ -z "$1" ] || ! [[ $1 =~ ^[0-9]+$ ]]; then
  echo "Usage: $0 <day>"
  exit 1
fi

folder_name="d$1"

# Create the folder if it does not exist
if [ ! -d "$folder_name" ]; then
  mkdir "$folder_name"
  echo "Folder '$folder_name' created."
else
  echo "Folder '$folder_name' already exists."
fi

cd $folder_name

touch test_input.txt
touch input.txt
echo "test = True
filePath = './test_input.txt' if test else './input.txt'

with open(filePath, 'r') as f:
    data = f.read()" > main_p1.py
touch main_p2.py