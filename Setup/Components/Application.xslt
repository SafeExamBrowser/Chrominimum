<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
    <xsl:output method="xml" indent="yes"/>
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>
    <xsl:template match="wix:File[substring(@Source, string-length(@Source) - string-length('Chrominimum.exe') + 1) = 'Chrominimum.exe']">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
            <xsl:attribute name="Id">
                <xsl:text>MainExecutable</xsl:text>
            </xsl:attribute>
        </xsl:copy>
        <wix:File Id="ApplicationIconFile" Source="Resources\Application.ico" />
    </xsl:template>
</xsl:stylesheet>