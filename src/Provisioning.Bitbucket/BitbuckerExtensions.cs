﻿using Cake.Core.Annotations;
using MK6.Tools.CakeBuild.Frosting;
using Provisioning.Bitbucket.Client;
using Provisioning.Bitbucket.Client.Models;
using System.Threading.Tasks;

namespace Provisioning.Bitbucket
{
    public static class BitbuckerExtensions
    {
        [CakeMethodAlias]
        public static async Task<Repository> BitbucketCreateRepositoryAsync(this Context context, BitbucketClientOptions clientOptions, string ownerUsername, string repositorySlug, string scm, bool isPrivate)
        {
            return await BitbucketCreateRepositoryAsync(context, new BitbucketCreateRepositoryOptions(clientOptions, ownerUsername, repositorySlug, scm, isPrivate));
        }

        [CakeMethodAlias]
        public static Repository BitbucketCreateRepository(this Context context, BitbucketClientOptions clientOptions, string ownerUsername, string repositorySlug, string scm, bool isPrivate)
        {
            return BitbucketCreateRepositoryAsync(context, clientOptions, ownerUsername, repositorySlug, scm, isPrivate).Result;
        }

        [CakeMethodAlias]
        public static async Task<Repository> BitbucketCreateRepositoryAsync(this Context context, BitbucketCreateRepositoryOptions options)
        {
            using (var client = new BitbucketClient(options))
            {
                return await client.CreateRepositoryAsync(options.OwnerUsername, options.RepositorySlug, options.NewRepository); 
            }
        }

        [CakeMethodAlias]
        public static Repository BitbucketCreateRepository(this Context context, BitbucketCreateRepositoryOptions options)
        {
            return BitbucketCreateRepositoryAsync(context, options).Result;
        }
    }

    public class BitbucketCreateRepositoryOptions : BitbucketClientOptions
    {
        public string OwnerUsername { get; set; }
        public string RepositorySlug { get; set; }
        public Repository NewRepository { get; set; }

        public BitbucketCreateRepositoryOptions(BitbucketClientOptions clientOptions, string ownerUsername, string repositorySlug, string scm, bool isPrivate)
        {
            NewRepository = new Repository { Scm = scm, IsPrivate = isPrivate };
            OwnerUsername = ownerUsername;
            RepositorySlug = repositorySlug;
            ServerUri = clientOptions.ServerUri;
            Username = clientOptions.Username;
            Password = clientOptions.Password;
        }
    }
        

}
