from django.urls import path
from .views import CategoryListView, CategoryDetailView, PostListView, PostDetailView

urlpatterns = [
    path('', CategoryListView.as_view()),
    path('categories/', CategoryListView.as_view()),
    path('categories/<pk>', CategoryDetailView.as_view()),
    path('posts/', PostListView.as_view()),
    path('posts/<pk>', PostDetailView.as_view()),
]