from .models import Category, Post
from  rest_framework import serializers

class CategorySerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Category
        fields = ('id','url','title','slug')


class PostSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Post
        fields = ('id','url','title', 'description', 'text', 'category', 'created_date', 'published_date', 'slug')