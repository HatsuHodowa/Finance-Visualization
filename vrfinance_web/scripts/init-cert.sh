#!/bin/bash
# Generate Let's Encrypt certificate using standalone mode
# Run this from anywhere: sudo scripts/init-cert.sh

set -e  # Exit on first error

EMAIL="dennischen5022@gmail.com"
DOMAIN="financialreality.photo"
WWW="www.financialreality.photo"

# Resolve absolute project path (this avoids $(pwd) issues with sudo)
PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

# Run certbot in standalone mode
echo "Requesting certificate for $DOMAIN and $WWW..."
sudo docker run -it --rm \
  -v "${PROJECT_DIR}/certs:/etc/letsencrypt" \
  -v "${PROJECT_DIR}/certs-data:/var/lib/letsencrypt" \
  -p 80:80 \
  certbot/certbot certonly --standalone \
  --email $EMAIL --agree-tos --no-eff-email \
  -d "$DOMAIN" -d "$WWW"
