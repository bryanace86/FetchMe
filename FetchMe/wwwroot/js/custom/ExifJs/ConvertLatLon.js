function dmsToDec(dms, direction) {

    if (direction != null) {
        direction.toUpperCase();

        var dd = dms[0].numerator + dms[1].numerator / (60 * dms[1].denominator) + dms[2].numerator / (3600 * dms[2].denominator);

        //var dd = days + minutes / 60 + seconds / (3600);
        //alert(dd);
        if (direction == "S" || direction == "W") {
            dd = dd * -1;
        } // Don't do anything for N or E
        return dd;
    }
}