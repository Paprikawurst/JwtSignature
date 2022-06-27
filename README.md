# LicenseLibrary


1. Create RSA Keypair on git bash  `openssl genrsa -out private.pem 2048` (adjust the rsa key length (here: 2048) as you wish)

2. extract public key from private key `openssl rsa -in private.pem -outform PEM -pubout -out public.pem`

3. place keys under `C:\keys\`

4. start program and copy token + public key onto `https://jwt.io/` with `RS512` as algorithm (RSA using SHA-512 hash algorithm) to verify signature
