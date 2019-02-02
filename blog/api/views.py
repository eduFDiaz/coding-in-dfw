from rest_framework import viewsets

from .serializers import CategorySerializer, PostSerializer
from blog.models import Category, Post

######### Category views ###################
class CategoryViewSet(viewsets.ModelViewSet):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer

######### Post views ###################
class PostViewSet(viewsets.ModelViewSet):
    queryset = Post.objects.all()
    serializer_class = PostSerializer