namespace TelemetryApp
{
    public class Consts
    {
        public const string SERVICE_PART_NAME_PROPERTY = "Служебная часть кадра";
        public const string INFO_PART_NAME_PROPERTY = "Информационная часть кадра";
        public const short HEADER_SIZE = 11;
        public const short WORD_SIZE = 5;
        public const short FRAME_BODY_SIZE = 543;
        public const short FRAME_SIZE = HEADER_SIZE + FRAME_BODY_SIZE * WORD_SIZE;
        public const short INFO_FRAME_PART_SIZE = 512;
        public const short SERVICE_FRAME_PART_SIZE = 31;
        public const short FRAME_COUNT = 1000;
        public const string FRAME_FILE_EXTENSION = "*.kdr";
    }
}