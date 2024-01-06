namespace POSWEB.Server.DataTransferObjets.Common
{
    public record struct LookupRecordStruct(uint Id, string Name);
    public record LookupRecord(uint Id, string Name);
    public struct LookupStruct(uint Id, string Name);
}
