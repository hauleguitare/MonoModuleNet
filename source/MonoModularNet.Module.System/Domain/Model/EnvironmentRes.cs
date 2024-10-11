﻿namespace MonoModularNet.Module.System.Domain.Model;

public class EnvironmentRes
{
    public EnvironmentRes()
    {
        Metadata = new EnvironmentMetadataRes();
    }

    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public object? Value { get; set; }
    
    public EnvironmentMetadataRes Metadata { get; set; }
}