from django.http import JsonResponse

# This is a placeholder for your real database models.
# In a real app, this data would come from your models.
DUMMY_TRANSACTIONS = [
    {"id": 1, "date": "2025-10-01", "amount": 55.50, "description": "Coffee Shop"},
    {"id": 2, "date": "2025-10-02", "amount": 1200.00, "description": "Monthly Salary"},
    {"id": 3, "date": "2025-10-03", "amount": 35.99, "description": "Online Subscription"},
]

def transaction_list(request):
    """
    Returns a list of transactions as JSON.
    """
    # The 'safe=False' argument is required when returning a JSON object
    # that is not a dictionary at its top level (like a list).
    return JsonResponse(DUMMY_TRANSACTIONS, safe=False)