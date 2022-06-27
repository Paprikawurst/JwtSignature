# LicenseLibrary


1. Create RSA Keypair on cmd  `openssl genrsa -out private.pem 2048`

2. extract public key from private key `openssl rsa -in private.pem -outform PEM -pubout -out public.pem`

3. place keys under `C:\keys\`

4. start program and copy token + public key onto `https://jwt.io/` with `RS512` as algorithm to verify signature
