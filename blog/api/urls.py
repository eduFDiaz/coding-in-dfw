from .views import CategoryViewSet, PostViewSet
from rest_framework.routers import DefaultRouter

router = DefaultRouter()
router.register(r'categories', CategoryViewSet, basename='category')
router.register(r'posts', PostViewSet, basename='post')
urlpatterns = router.urls
print(urlpatterns)