namespace BTG.CasePricing.Domain.Interfaces.DTOs.Responses
{
    public class GitRepositorioResponse
    {
        public int Total_count { get; set; }
        public bool Incomplete_results { get; set; }
        public List<Items>? items { get; set; }

        public class Items
        {   
            public int Id { get; set; }
            public string? Node_id { get; set; }
            public string? Name { get; set; }
            public string? Full_name { get; set; }
            
            public Owner? Owner { get; set; }
            public string? Html_url { get; set; }
            public string? Description { get; set; }
            public bool? Fork { get; set; }
            public string? Url { get; set; }
            public string? Forks_url { get; set; }
            public string? Keys_url { get; set; }
            public string? Collaborators_url { get; set; }
            public string? Teams_url { get; set; }
            public string? Hooks_url { get; set; }
            public string? Issue_events_url { get; set; }
            public string? Events_url { get; set; }
            public string? Assignees_url { get; set; }
            public string? Branches_url { get; set; }
            public string? Tags_url { get; set; }
            public string? Blobs_url { get; set; }
            public string? Git_tags_url { get; set; }
            public string? Git_refs_url { get; set; }
            public string? Trees_url { get; set; }
            public string? Statuses_url { get; set; }
            public string? Languages_url { get; set; }
            public string? Stargazers_url { get; set; }
            public string? Contributors_url { get; set; }
            public string? Subscribers_url { get; set; }
            public string? Subscription_url { get; set; }
            public string? Commits_url { get; set; }
            public string? Git_commits_url { get; set; }
            public string? Comments_url { get; set; }
            public string? Issue_comment_url { get; set; }
            public string? Contents_url { get; set; }
            public string? Compare_url { get; set; }
            public string? Merges_url { get; set; }
            public string? Archive_url { get; set; }
            public string? Downloads_url { get; set; }
            public string? Issues_url { get; set; }
            public string? Pulls_url { get; set; }
            public string? Milestones_url { get; set; }
            public string? Notifications_url { get; set; }
            public string? Labels_url { get; set; }
            public string? Releases_url { get; set; }
            public string? Deployments_url { get; set; }
            public DateTime Created_at { get; set; }
            public DateTime Updated_at { get; set; }
            public DateTime Pushed_at { get; set; }
            public string? Git_url { get; set; }
            public string? Ssh_url { get; set; }
            public string? Clone_url { get; set; }
            public string? Svn_url { get; set; }
            public string? Homepage { get; set; }
            public int Size { get; set; }
            public int Stargazers_count { get; set; }
            public int Watchers_count { get; set; }
            public string? Language { get; set; }
            public bool Has_issues { get; set; }
            public bool Has_projects { get; set; }
            public bool Has_downloads { get; set; }
            public bool Has_wiki { get; set; }
            public bool Has_pages { get; set; }
            public int Forks_count { get; set; }
            public object? Mirror_url { get; set; }
            public bool Archived { get; set; }
            public bool Disabled { get; set; }
            public int Open_issues_count { get; set; }
            public License? License { get; set; }
            public bool Allow_forking { get; set; }
            public bool Is_template { get; set; }
            public bool Web_commit_signoff_required { get; set; }
            public List<string>? Topics { get; set; }
            public string? Visibility { get; set; }
            public int Forks { get; set; }
            public int Open_issues { get; set; }
            public int Watchers { get; set; }
            public string? Default_branch { get; set; }
            public double Score { get; set; }
        }

        public class License
        {
            public string? Key { get; set; }
            public string? Name { get; set; }
            public string? Spdx_id { get; set; }
            public string? Url { get; set; }
            public string? Node_id { get; set; }
        }

        public class Owner
        {
            public string? Login { get; set; }
            public int Id { get; set; }
            public string Node_id { get; set; }
            public string? Avatar_url { get; set; }
            public string? Gravatar_id { get; set; }
            public string? Url { get; set; }
            public string? Html_url { get; set; }
            public string? Followers_url { get; set; }
            public string? Following_url { get; set; }
            public string? Gists_url { get; set; }
            public string? Starred_url { get; set; }
            public string? Subscriptions_url { get; set; }
            public string? Organizations_url { get; set; }
            public string? Repos_url { get; set; }
            public string? Events_url { get; set; }
            public string? Received_events_url { get; set; }
            public string? Type { get; set; }
            public bool Site_admin { get; set; }
        }

    }
}
