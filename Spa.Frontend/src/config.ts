const config = {
  baseApiUrl: 'https://localhost:4000',
  jwtToken:
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6ImFkbWluIiwicm9sZSI6ImFkbWluIiwiRmF2b3JpdGVDb2xvciI6ImdyYXkiLCJuYmYiOjE2NjI5OTgwNjQsImV4cCI6MTY2MzAwMTY2NCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDAwMCIsImF1ZCI6IlNwYS5BUEkifQ.AISEksR1PVQpjgLCObDAVuhtYtL5vU_6vFhflC5Nu2w',
};

export const currencyFormatter = Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
  maximumFractionDigits: 0,
});

export default config;
