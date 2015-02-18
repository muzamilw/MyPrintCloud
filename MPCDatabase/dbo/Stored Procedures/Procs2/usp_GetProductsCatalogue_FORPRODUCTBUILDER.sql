﻿create PROCEDURE [dbo].[usp_GetProductsCatalogue_FORPRODUCTBUILDER]
  @CATEGORYID int
AS
BEGIN
   SELECT *,
		(SELECT COUNT(LABELMAP1) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL1,
		(SELECT COUNT(LABELMAP2) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL2,
		(SELECT COUNT(LABELMAP3) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL3,
		(SELECT COUNT(LABELMAP4) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL4,
        (SELECT COUNT(LABELMAP5) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL5,
        (SELECT COUNT(LABELMAP6) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL6,
        (SELECT COUNT(LABELMAP7) FROM webProductCategoryItem WHERE CATEGORYID = @CATEGORYID) AS TOTAL7
FROM webProductCategoryItem WPCI
WHERE CATEGORYID = @CATEGORYID

END