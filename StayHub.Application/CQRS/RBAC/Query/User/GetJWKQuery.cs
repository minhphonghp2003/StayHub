using MediatR;
using Microsoft.IdentityModel.Tokens;
using Shared.Response;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using System.Security.Cryptography;

namespace StayHub.Application.CQRS.RBAC.Query.User
{
    // Include properties to be used as input for the query
    public record GetJWKQuery() : IRequest<JWKSetDTO>;
    public sealed class GetJWKQueryHandler(ISigningKeyRepository signingKeyRepository) : BaseResponseHandler, IRequestHandler<GetJWKQuery, JWKSetDTO>
    {
        public async Task<JWKSetDTO> Handle(GetJWKQuery request, CancellationToken cancellationToken)
        {
            var keys = await signingKeyRepository.GetManyAsync(e => e.IsActive == true, (e, i) => new JWKDTO()
            {
                kty = "RSA",    // Key type (RSA)
                use = "sig",    // Usage (sig for signature)
                kid = e.Id.ToString(),  // Key ID to identify the key
                alg = "RS256",  // Algorithm (RS256 for RSA SHA-256)
                n = Base64UrlEncoder.Encode(GetModulus(e.PublicKey)), // Modulus (Base64URL-encoded)
                e = Base64UrlEncoder.Encode(GetExponent(e.PublicKey)) // Exponent (Base64URL-encoded)
            },null, false);
            return new JWKSetDTO()
            {
                keys = keys.ToList()
            };
        }
        private byte[] GetModulus(string publicKey)
        {
            // Create a new RSA instance for cryptographic operations
            var rsa = RSA.Create();
            // Import the RSA public key from its Base64-encoded representation
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            // Export the RSA parameters without including the private key
            var parameters = rsa.ExportParameters(false);
            // Dispose of the RSA instance to free up resources and prevent memory leaks
            rsa.Dispose();
            if (parameters.Modulus == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }
            // Return the modulus component of the RSA key
            return parameters.Modulus;
        }
        // Helper method to extract the exponent component from a Base64-encoded public key
        private byte[] GetExponent(string publicKey)
        {
            // Create a new RSA instance for cryptographic operations
            var rsa = RSA.Create();
            // Import the RSA public key from its Base64-encoded representation
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            // Export the RSA parameters without including the private key
            var parameters = rsa.ExportParameters(false);
            // Dispose of the RSA instance to free up resources and prevent memory leaks
            rsa.Dispose();
            if (parameters.Exponent == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }
            // Return the exponent component of the RSA key
            return parameters.Exponent;
        }
    }

}
