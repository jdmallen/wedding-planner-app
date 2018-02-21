using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models.Domain;

namespace WeddingPlanner.DataAccess.Impls
{
	public class AppUserRepository: IUserClaimStore<AppUser>,
									IUserLoginStore<AppUser>,
									IUserPasswordStore<AppUser>
	{
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the user identifier for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose identifier should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user" />.</returns>
		public async Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the user name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose name should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.</returns>
		public async Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose name should be set.</param>
		/// <param name="userName">The user name to set.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		public async Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the normalized user name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose normalized name should be retrieved.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.</returns>
		public async Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Sets the given normalized name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose name should be set.</param>
		/// <param name="normalizedName">The normalized name to set.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		public async Task SetNormalizedUserNameAsync(
			AppUser user,
			string normalizedName,
			CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Creates the specified <paramref name="user" /> in the user store.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the creation operation.</returns>
		public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Updates the specified <paramref name="user" /> in the user store.
		/// </summary>
		/// <param name="user">The user to update.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.</returns>
		public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Deletes the specified <paramref name="user" /> from the user store.
		/// </summary>
		/// <param name="user">The user to delete.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.</returns>
		public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
		/// </summary>
		/// <param name="userId">The user ID to search for.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
		/// </returns>
		public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Finds and returns a user, if any, who has the specified normalized user name.
		/// </summary>
		/// <param name="normalizedUserName">The normalized user name to search for.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName" /> if it exists.
		/// </returns>
		public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the specified <paramref name="user" /> as an asynchronous operation.
		/// </summary>
		/// <param name="user">The role whose claims to retrieve.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
		/// </returns>
		public async Task<IList<Claim>> GetClaimsAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>Add claims to a user as an asynchronous operation.</summary>
		/// <param name="user">The user to add the claim to.</param>
		/// <param name="claims">The collection of <see cref="T:System.Security.Claims.Claim" />s to add.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task AddClaimsAsync(AppUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Replaces the given <paramref name="claim" /> on the specified <paramref name="user" /> with the <paramref name="newClaim" />
		/// </summary>
		/// <param name="user">The user to replace the claim on.</param>
		/// <param name="claim">The claim to replace.</param>
		/// <param name="newClaim">The new claim to replace the existing <paramref name="claim" /> with.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task ReplaceClaimAsync(
			AppUser user,
			Claim claim,
			Claim newClaim,
			CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to remove the specified <paramref name="claims" /> from.</param>
		/// <param name="claims">A collection of <see cref="T:System.Security.Claims.Claim" />s to remove.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		public async Task RemoveClaimsAsync(AppUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Returns a list of users who contain the specified <see cref="T:System.Security.Claims.Claim" />.
		/// </summary>
		/// <param name="claim">The claim to look for.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <typeparamref name="TUser" /> who
		/// contain the specified claim.
		/// </returns>
		public async Task<IList<AppUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Adds an external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to add the login to.</param>
		/// <param name="login">The external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to add to the specified <paramref name="user" />.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		public async Task AddLoginAsync(AppUser user, UserLoginInfo login, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Attempts to remove the provided login information from the specified <paramref name="user" />.
		/// and returns a flag indicating whether the removal succeed or not.
		/// </summary>
		/// <param name="user">The user to remove the login information from.</param>
		/// <param name="loginProvider">The login provide whose information should be removed.</param>
		/// <param name="providerKey">The key given by the external login provider for the specified user.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		public async Task RemoveLoginAsync(
			AppUser user,
			string loginProvider,
			string providerKey,
			CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Retrieves the associated logins for the specified <param ref="user" />.
		/// </summary>
		/// <param name="user">The user whose associated logins to retrieve.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the specified <paramref name="user" />, if any.
		/// </returns>
		public async Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Retrieves the user associated with the specified login provider and login provider key.
		/// </summary>
		/// <param name="loginProvider">The login provider who provided the <paramref name="providerKey" />.</param>
		/// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing the user, if any which matched the specified login provider and key.
		/// </returns>
		public async Task<AppUser> FindByLoginAsync(
			string loginProvider,
			string providerKey,
			CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Sets the password hash for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose password hash to set.</param>
		/// <param name="passwordHash">The password hash to set.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		public async Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the password hash for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose password hash to retrieve.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user" />.</returns>
		public async Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets a flag indicating whether the specified <paramref name="user" /> has a password.
		/// </summary>
		/// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
		/// <returns>
		/// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a password
		/// otherwise false.
		/// </returns>
		public async Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}
}