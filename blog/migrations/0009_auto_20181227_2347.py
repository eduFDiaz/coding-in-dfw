# Generated by Django 2.1 on 2018-12-28 05:47

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('blog', '0008_category_description'),
    ]

    operations = [
        migrations.AlterField(
            model_name='category',
            name='image',
            field=models.ImageField(default='categories/images/no-img.jpg', upload_to='categories/images/'),
        ),
    ]