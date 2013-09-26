using System;

namespace Dypsok
{
    public class NoMoreProductIdsAvailableException : Exception { }

    public class DuplicateProductIdException: Exception { }

    public class PaymentScheduleRequiredException : Exception { }

    public class TooManyFareZonesException : Exception { }

}
