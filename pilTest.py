from PIL import Image
data = 'docker.png'
size = (230, 230)
image = Image.open(data)
image.thumbnail(size, Image.ANTIALIAS)
#background = Image.new('RGBA', size, (0,0,0, 0))
#background.paste(
#    image, (int((size[0] - image.size[0]) / 2), int((size[1] - image.size[1]) / 2))
#)
#background.save("output.png")
image.save("output.png")