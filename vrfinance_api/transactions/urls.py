from django.urls import path
from . import views

urlpatterns = [
    # Maps requests to /api/transactions/ to the transaction_list view
    path('', views.transaction_list, name='transaction-list'),
]