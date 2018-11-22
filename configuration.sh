# Create a system user named coding assign it to a system group called webapps
sudo groupadd --system webapps
sudo useradd --system --gid webapps --home /webapps/codingindfw-env coding

# Now change the owner of files in each application’s folder. I like to assign the group users as the owner, because that allows regular users of the server to access and modify parts of the application which are group-writable. This is optional.
sudo chown -R coding:users /webapps/codingindfw-env
