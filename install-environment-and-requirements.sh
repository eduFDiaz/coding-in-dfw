#!/usr/bin/env bash

# install python 3.6.6
sudo apt-get update
sudo apt-get install build-essential tk-dev libncurses5-dev libncursesw5-dev libreadline6-dev libdb5.3-dev libgdbm-dev libsqlite3-dev libssl-dev libbz2-dev libexpat1-dev liblzma-dev zlib1g-dev
wget https://www.python.org/ftp/python/3.6.6/Python-3.6.6.tar.xz
tar xf Python-3.6.6.tar.xz
cd Python-3.6.6
./configure --enable-optimizations
make -j4
sudo make -j4 altinstall

# cleanup the install files and packages
cd ..
sudo rm -r Python-3.6.6
rm Python-3.6.6.tgz
sudo apt-get --purge remove -y build-essential tk-dev
sudo apt-get --purge remove -y libncurses5-dev libncursesw5-dev libreadline6-dev
sudo apt-get --purge remove -y libdb5.3-dev libgdbm-dev libsqlite3-dev libssl-dev
sudo apt-get --purge remove -y libbz2-dev libexpat1-dev liblzma-dev zlib1g-dev
sudo apt-get autoremove -y
sudo apt-get clean

# install pip
sudo apt install python3-pip

# virtual env installation
sudo pip3.6 install virtualenv

# create virtual env
virtualenv -p python3.6 codingindfw-env

# activate the environment
source codingindfw-env/bin/activate

# install requirements and dependencies
sudo apt-get install -y libjpeg-dev zlib1g-dev
sudo pip3.6 install -r coding-in-dfw/requirements.txt

# in windows
# virtualenv -p "C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\python.exe" newenv
#.\newenv\Scripts\activate
# pip install -r requirements.txt
