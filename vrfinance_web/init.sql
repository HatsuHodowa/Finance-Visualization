CREATE TABLE IF NOT EXISTS statements (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL,
  start_date DATE,
  end_date DATE
);

CREATE TABLE IF NOT EXISTS transactions (
  id SERIAL PRIMARY KEY,
  statement_id INTEGER REFERENCES statements(id) ON DELETE CASCADE,
  date DATE,
  amount NUMERIC,
  description TEXT,
  category TEXT
);

