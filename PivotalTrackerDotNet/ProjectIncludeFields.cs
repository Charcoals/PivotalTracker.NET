using System;

namespace PivotalTrackerDotNet
{
    [Flags]
    public enum ProjectIncludeFields
    {
        Default                     = 0,
        AccountingType              = 1,
        Featured                    = 2,
        StoryIds                    = 4,
        Epics                       = 8,
        EpicIds                     = 16,
        MembershipIds               = 32,
        Memberships                 = 64,
        LabelIds                    = 128,
        Labels                      = 256,
        IntegrationIds              = 512,
        IterationOverrideNumbers    = 1024,
        Integrations                = 2048,
        Stories                     = 4096
    }
}