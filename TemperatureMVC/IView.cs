namespace Temperature
{
    interface IView
    {
        void RefreshView(double temperature);

        void SetFrom(string measurementUnit);

        void SetTo(string measurementUnit);

        void SetValue(double temperature);
    }
}
