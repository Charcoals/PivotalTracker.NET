using System;

namespace PivotalTrackerDotNet
{
    [Flags]
    public enum StoryIncludeFields
    {
        Default                     = 0,
        TaskIds                     = 1,
        Tasks                       = 2,
        FollowerIds                 = 4,
        Followers                   = 8,
        CommentIds                  = 16,
        Comments                    = 32,
        BeforeId                    = 64,
        AfterId                     = 128,
        OwnerIds                    = 256,
        //Owners                      = 512
    }
}