# Personal Finance Budgeting App

A full-stack personal finance application that allows users to manage their bank statements and track transactions. Built with Express.js backend and React frontend.

## Features

- **User Authentication**: Register and login with JWT-based authentication
- **CSV Upload**: Upload bank statement CSV files with automatic parsing
- **Transaction Management**: View and manage transactions from uploaded statements
- **Statement Management**: List and delete uploaded bank statements
- **Responsive Design**: Modern, mobile-friendly interface

## Tech Stack

### Backend
- Node.js with Express.js
- SQLite database
- JWT authentication
- Multer for file uploads
- CSV parser for bank statements

### Frontend
- React 18
- React Router for navigation
- Axios for API calls
- React Dropzone for file uploads
- React Toastify for notifications

## Getting Started

### Prerequisites
- Node.js (v14 or higher)
- npm or yarn

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd HackNC25
   ```

2. **Install backend dependencies**
   ```bash
   cd backend
   npm install
   ```

3. **Install frontend dependencies**
   ```bash
   cd ../frontend
   npm install
   ```

### Running the Application

1. **Start the backend server**
   ```bash
   cd backend
   npm run dev
   ```
   The backend will run on http://localhost:5000

2. **Start the frontend development server**
   ```bash
   cd frontend
   npm start
   ```
   The frontend will run on http://localhost:3000

3. **Open your browser**
   Navigate to http://localhost:3000 to use the application

## Usage

### 1. User Registration/Login
- Create a new account or login with existing credentials
- JWT tokens are automatically managed for authentication

### 2. Upload Bank Statements
- Navigate to "Upload Statement" page
- Drag and drop or select a CSV file
- The app will automatically parse and import transactions

### 3. View Transactions
- Go to "Statements" to see all uploaded files
- Click "View Transactions" to see detailed transaction lists
- Transactions are displayed with date, description, and amount

### 4. Manage Statements
- Delete statements and their associated transactions
- View transaction summaries and totals

## CSV Format Requirements

Your bank statement CSV files should have the following columns:
- `date` - Transaction date (YYYY-MM-DD or MM/DD/YYYY format)
- `description` - Transaction description/memo
- `amount` - Transaction amount (positive or negative numbers)

Example CSV format:
```csv
date,description,amount
2024-01-15,Grocery Store,-45.67
2024-01-16,Salary Deposit,2500.00
2024-01-17,Gas Station,-32.10
```

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login

### Statements
- `POST /api/statements/upload` - Upload CSV statement
- `GET /api/statements` - Get user's statements
- `GET /api/statements/:id/transactions` - Get transactions for a statement
- `DELETE /api/statements/:id` - Delete statement and transactions

## Database Schema

### Users Table
- id (Primary Key)
- username (Unique)
- email (Unique)
- password (Hashed)
- created_at

### Bank Statements Table
- id (Primary Key)
- user_id (Foreign Key)
- filename
- uploaded_at

### Transactions Table
- id (Primary Key)
- statement_id (Foreign Key)
- date
- description
- amount

## Security Features

- Password hashing with bcrypt
- JWT token authentication
- File type validation for uploads
- User-specific data isolation
- Input validation and sanitization

## Development

### Backend Development
```bash
cd backend
npm run dev  # Uses nodemon for auto-restart
```

### Frontend Development
```bash
cd frontend
npm start    # React development server with hot reload
```

### Database
The SQLite database is automatically created when you first run the backend. The database file (`database.sqlite`) will be created in the backend directory.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is licensed under the MIT License.