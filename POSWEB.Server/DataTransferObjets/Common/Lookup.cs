namespace POSWEB.Server.DataTransferObjets.Common
{
    public record struct LookupRecordStruct(int Id, string Name);
    public record struct LookupRecordShortStruct(int Id, string Name);
    public record LookupRecord(int Id, string Name);
    public struct LookupStruct(int Id, string Name);
}
