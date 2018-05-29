<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output encoding="UTF-8" indent="yes" method="html"/>
    <xsl:template match="/">
        <html>
            <head>
                <title>Complete List of Songs</title>
            </head>
            <body>
                <h2>Complete List of Songs</h2>
                <xsl:apply-templates select="cdlibrary"/>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="cdlibrary">
        <xsl:for-each select="cd">
            <br/>
            <font color="green">
                <xsl:value-of select="cdtitle"/> , <xsl:value-of select="cdlabel"/> , <xsl:value-of
                    select="cdyear"/> [ <xsl:value-of select="cdid"/>] </font>
            <br/>
            <table>
                <xsl:for-each select="track">
                    <tr>
                        <td style="text-align:left">
                            <xsl:value-of select="trknum"/>
                        </td>
                        <td>
                            <xsl:value-of select="trktitle"/>
                        </td>
                        <td style="text-align:center">
                            <xsl:value-of select="trklen"/>
                        </td>
                    </tr>
                </xsl:for-each>
            </table>
            <br/>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
