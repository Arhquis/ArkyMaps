using DM = ArkyMapsDomainModel;
using EDM = ArkyMapsDal;

namespace ArkyMapsDal
{
    public class PositionService
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="PositionService"/> class.
        /// </summary>
        internal PositionService()
        {
            // NOTE intentionally left blank
        }
        #endregion


        #region position
        /// <summary>
        /// Creates a new <see cref="Position"/> entity.
        /// </summary>
        /// <param name="dmPosition">A <see cref="DM.Position"/> entity to save in database.</param>
        /// <returns>True if saving was successfull, false otherwise.</returns>
        public bool CreatePosition(DM.Position dmPosition)
        {
            bool rvSucceeded = false;

            using (ArkyMapsDatabaseEntities context = new ArkyMapsDatabaseEntities())
            {
                EDM.Position edmPosition = context.Position.Create();
                edmPosition.PhoneUserId = dmPosition.PhoneUserId;
                edmPosition.Longitude = dmPosition.Location.Longitude;
                edmPosition.Latitude = dmPosition.Location.Latitude;
                edmPosition.Timestamp = dmPosition.Timestamp;

                context.Position.Add(edmPosition);

                int affectedRows = context.SaveChanges();

                if (affectedRows == 1)
                {
                    rvSucceeded = true;
                }
            }

            return rvSucceeded;
        }
        #endregion
    }
}
